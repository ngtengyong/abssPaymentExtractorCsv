# ABSSPaymentExtractor

## Objective
To extract "Bank Register" records in ABSS (IRSB Accounting application) into a csv file that can be easily uploaded to Maybank web app for fund transfer to various suppliers.

![figure1](http://gitlabnew.interbase.com.my/superbuy.misc/absspaymentextractor/raw/master/img/Picture1.png "picture 1")

## Installation
* Install Abss software on user’s PC
* Ensure .net framework 4.5 is present on user’s PC
* Create System DSN entry and name it as “InterbaseSvr”, ensure the driver name is ABSSMY1611 for ABSS version 22

![figure2](http://gitlabnew.interbase.com.my/superbuy.misc/absspaymentextractor/raw/master/img/Picture2.png "picture 2")

* Create a folder in C:\AbssMBBPayExtractor (Can be any folder) and copy the program files into it.
* The program only consists of 2 files thus far,
	1. AbssMaybankPayment.exe
	1. AbssMaybankPayment.exe.config



(open the .config fie and ensure the value of “AbssDSN” is same as the name of DSN entry)

![figure3](http://gitlabnew.interbase.com.my/superbuy.misc/absspaymentextractor/raw/master/img/Picture3.png "picture 3")

* Create a shortcut to AbssMaybankPayment.exe in Desktop for ease of access

## Run the application
* Double click AbssMaybankPayment.exe
* Enter username and password
* Enter Bank Account Code
* Enter Date of Bank Transfer

![figure4](http://gitlabnew.interbase.com.my/superbuy.misc/absspaymentextractor/raw/master/img/Picture4.png "picture 4")

* Press Enter and the program takes about a minute to complete the extraction cycle.
*Once the program is done extracting, it shows number of records being exported.
*The exported files are located in the output folder of the C:\AbssMBBPayExtractor

![figure5](http://gitlabnew.interbase.com.my/superbuy.misc/absspaymentextractor/raw/master/img/Picture5.png "picture 5")