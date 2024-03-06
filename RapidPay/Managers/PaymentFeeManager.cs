using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RapidPay.Business
{
    public class PaymentFeeManager
    {
        private static PaymentFeeManager instance;
        private static readonly object lockObject = new object();
        private static readonly Random random = new Random();
        private readonly IConfiguration Configuration;

        private decimal currentFee;
        private Timer timer;     
        private DateTime nextRuntime;
        private double nextRunInterval;

        public PaymentFeeManager(IConfiguration configuration)
        {
          
            currentFee = GenerateRandomFee();
            Configuration = configuration;
            var interval = Configuration["PaymentFeeUpdater:IntervalInHours"];
            nextRunInterval = GetServiceRunInterval(interval);
            timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromHours(nextRunInterval));
        }

        public string GetCurrentFeeAndNextUpdateSpan()
        {
            TimeSpan nextUpdate = nextRuntime - DateTime.Now;
            string fee = string.Format("Current Fee is : {0}, Next update happens in : {1} hours, {2} minutes and {3} seconds", currentFee, nextUpdate.Hours, nextUpdate.Minutes, nextUpdate.Seconds);
            return fee;
        }
        public decimal GetCurrentFee()
        {
            return currentFee;
        }

        public void UpdateFee(object? state)
        {

            decimal randomNumber = GenerateRandomFee();
            if (currentFee != 0) // Making sure we don't get stuck with 0
            {
                currentFee = decimal.Round(currentFee * randomNumber, 2);
            }
            else
            {
                currentFee = randomNumber;
            }       
            nextRuntime = DateTime.Now.AddHours(nextRunInterval);


        }

        private decimal GenerateRandomFee()
        {
            decimal randomNumber = (decimal)random.Next(0, 200) / 100;
            return  decimal.Round(randomNumber, 2);
        }

        public static PaymentFeeManager GetInstance(IConfiguration configuration)
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new PaymentFeeManager(configuration);
                    }
                }
            }
            return instance;
        }

        private double GetServiceRunInterval(string interval)
        {
            if (double.TryParse(interval, out double serviceInterval))
            {
                timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromHours(serviceInterval));
            }
            else
            {
                serviceInterval = 1; // defaults to 1
                timer = new Timer(UpdateFee, null, TimeSpan.Zero, TimeSpan.FromHours(serviceInterval));
            }

            return serviceInterval;

        }
    }
}
