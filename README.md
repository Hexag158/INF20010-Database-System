# INF20010 - Database Systems

This repository contains my work on assignments for a course INF20010 on Database Systems and Data Warehousing.

## Learning Objectives

1. Explain how a relational database system facilitates transaction and concurrency management.
2. Develop a concurrent, multi-user business application using stored procedures, stored functions, triggers, and a form-based host application.
3. Explain the need for and the fundamentals of data warehousing, ETL processes, data mining, and business intelligence systems.
4. Demonstrate the ability to design, build, and implement a simple data warehouse system.
5. Communicate effectively as a professional and function as an effective leader or member of a diverse team.

## Assignments

### Assignment 1:

#### Overview
In this assignment, I developed a concurrent, multi-user business application using stored procedures, functions (SPFs), triggers in Oracle DB, and a form-based host application.

#### Details
1. **Database Setup**: Created tables to store product and customer data.
2. **Stored Procedures and Functions (SPFs)**: Developed some SPFs to perform the following operations: Insert/Update/Delete/Query data
3. **Execution of SPFs**: These SPFs were called from:
    - Additional stored procedures can be executed from anonymous blocks via SQL Developer.
    - A host application written in C#.
4. **Database Transactions**: Some SPFs modified data in multiple rows in multiple tables, demonstrating the use of handling database transactions.
5. **Data Handling Using Cursors**: Some SPFs required data to be passed/returned using cursors.

### Assignment 2: Creating a Data Warehouse in Oracle DB

#### Overview
This assignment aimed to create a data warehouse, extract data from source tables, transform and clean the data, log all changes, and load the data into a data warehouse. Finally, I wrote queries based on data stored in the data warehouse.

#### Details
1. **Data Warehouse Creation**: Created a data warehouse.
2. **Data Extraction**: Extracted data from source tables.
3. **Data Transformation and Cleaning**: Transformed and cleaned the data, logging all changes.
4. **Data Loading**: Loaded the data into the data warehouse.
5. **Query Writing**: Wrote queries based on data stored in the data warehouse.

#### Notes
1. **ETL Operations**: All operations of ETL were achieved by running the `DW_Creation_Script.SQL` scripts. This script works on any source data values.
2. **SQL Usage**: The script only contains standard SQL, no PL/SQL was used (i.e., no stored procedures, functions, or anonymous blocks).
