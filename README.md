# UATP-RapidPay
UATP- Take Home Code Test

Assumptions :
I have made some assumptions while creating this program to define the scope. 
I refrained from including features such as adding names, addresses, or CVVs for cards. Instead, the focus was solely on operations related to adding a card using a card number, as well as functionalities for checking balance and making payments.
Writing API test cases was also assumed to be out of scope for this development.

I did not implement user or role management for generating AOI tokens. Instead, the API expects users to provide an X-auth-token for authorization. 
For the current scope of work, please use a hardcoded token : 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9:Admin' to validate your resquests. In an ideal scenario, a separate service or application would handle token generation and provision for authentication and authorization.


Before you run the Program, Please follow the below steps:
I am utilizing SQL Server as the database.
1. Please create the database using the provided SQL fil on your server : RapidpayDb.sql
2.Once you have created the database in your environment, please Update the 'appSettings.json' file located at 'UATP\RapidPay' to specify the correct database connection details. Update the 'DefaultConnection' key within this file accordingly.


Additional details :
1)Some of the errors are deliberatly kept as ambiguous, so as to not give too many details to the external user. I have added comments to those parts
2) I am saving the card in encrypted form in DB. Again, for scope of the ticket, the keys are hard coded in the program, which will not be true for an ideal scenario
3) I did not create additional class libraries for each layer, but that should be created to separate the conerns for each layer. I am using folder structure to do that here.

Testing APIs
You can use in-build swagger to test the API or Choose your own app, Here are some valid requets :


Headers : stays same for all 
X-Auth-Token : **eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9:Admin**
Content-Type : application/json


POST : https://servername/api/v1/Card   : This will create a card, saves the ecrypted card number in Db and also returns the encrypted card number
Body : 
{
  "cardNumber": "426132561238626",
  "balance": 0
}


GET : https://servername/api/v1/card?cardNumber=426132561238626


PUT : https://servername/api/v1/Card    : **This requires authorization of user role as Admin. So, make sure your user role is Admin(case-sensitive) in the Auth token**

Body :
{
  "cardNumber": "426132561238626",
  "amount": "200.1"
}



Feel free to reach out to me if you have any additional questions


