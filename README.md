Application has been written using .NET Core 2.2 version and C# programming language.  

As a templates I used Class Library for PaymentLibrary and ASP.NET Core Web API as a application that tests whether  
PaymentLibrary works correctly. In Solution Items folder you can find Curl.txt file that will let you test PaymentLibrary.  
If you want to test the application just start it and begin create calls using Curl.txt file.  

Aa a test case in appsettings.json file we are using PaymentApiSettings. For Productions this settings should be removed  
from settings file and instead of appsettings.json we can use some key-vault server that will let us save sensetive data.  

Application has 2 main part PaymentApp that will be used for testing, and PaymentLibrary that is can be used as a separate package.  

PaymentLibrary has the following projects:  
1 Payment.API - class library that will communicate with third party Payment Library.  
2 Payment.Interface - class library that contains signatures that can be used by client of this application  
3 Payment.Service - class library that contains Bussines Logic implementation.  
