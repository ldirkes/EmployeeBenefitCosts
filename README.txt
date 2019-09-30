Author: Lane Dirkes

Setup:
  This project operates on a local SQL database. In order to run it, you will need to create a local 
  database named People using Microsoft SQL Server Management Studio. Within that database, create a
  table named dbo.PeopleTable. You will need columns named employeeID, personName, hasDiscount, and 
  isDependant. Finally, update the connectionString in DatabaseManagement.cs with your server name.
  
Operation:
  There are 4 operations: search, add, remove, edit. In order to do one of these operations, fill out
  the fields as desired and then click the execute button.
  
  Search:
    There are three possible searches of the database. First, if you leave the input fields blank and
    execute a search, it will return the entire database to the UI. Second, you can search by employeeID.
    This will return all rows with that employee. These will be one employee and all of their dependents.
    Finally, you can do an exact search by filling out the employeeID and name fields and selecting the
    person's dependency state. This will return the single person who matches your criteria.
    
    When you execute a employeeID or specific person search, the UI will show you four values:
      1. The number of dependents that employeeID has
      2. If the employee is eligble for a discount
      3. Per paycheck deduction
      4. Paycheck employee will receive after deduction
    If the specific person you search for is a dependent, it still shows you the statistics 
    as if you had searched everyone tied to that employeeID.
  
  Add:
    Fill out input fields with the person's information, select the Add radio button and click the Execute button.
  
  Remove:
    Fill out input fields with the person's information, select the Remove radio button and click the Execute button.
    
  Edit:
    Fill out both sets of input fields with the original person's information and the new information
    you would like to be stored. Select the Remove radio button and click the Execute button.
    
  Addtionaly, you can set the percentage of the cost of benefits that the company pays for using the
  by entering it in the Company Contribution Ratio box and clicking the Update button.
  
Known Issues:
  Remove will silently fail if you do not enter a valid person that exists in the database.
  Edit does not have fully fleshed out input validation.
