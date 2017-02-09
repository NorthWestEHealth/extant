Deploying Extant to an IIS instance under Windows Server:

1) Install SQL Server with management tools. 
2) Using management tools, connect to SQL server, create a new blank database (by convention called Extant, although this is not essential)
3) Open the script create_extant.sql, connecting it to youre SQL server instance ands ensuring that the current database is the blank one created above.
4) execute the query. Check that this has created new tables in your blank database.

5) Create a new website in IIS, ensuring that it uses an application pool set up for .NET 4.
6) Copy all content from the Website folder into the root of the new website.
7) Check that the connectionString in web.config matches the details of the database you have created
	- the default assumes an SQL Server Express installation and a database named "Extant" - if this is not the case you will need to change web.config.

8) Check that the application pool identity has read and write access to both the Extant database and the website root folder. 
	- The easiest way to do this is to create a new local user in Windows and give it the appropriate permissions.

9) the application should now be ready to use. Open the website homepage using a browser locally on the server: this will provide access to a Setup
	page that allows you to set the basic information used by the application, as well as creating an adminstrator account.