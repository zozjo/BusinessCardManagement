# Assignment description

# Phone Book

Write a .Net Web application to manage the business cards information in phone book access database, having the following requirements:

1. Business card information includes:
  * Name.
  * Gender.
  * Date of birth.
  * Email.
  * Phone.
  * Photo - optional field (Max size is 1MB) .
  * Address.
  
2. The first page shall enable the user to add new business cards
  * Business card information can be add in four ways:
    - Through UI.
    - Imported from xml file. (sample file BusinessCard.xml)
    - Imported from csv file. (sample file BusinessCard.csv)
    - (OPTIONAL) Imported from QR code image -without photo-. (sample image BusinessCard.png).
  * After importing a file, the user shall be able to view the business card information before submitting the add request.
  * Photo is encoded as base64 string in all formats.
  
3. The second page shall list all stored business card information.
  * The user shall be able to delete any selected business card.
  * The user shall be able to export the selected business card to (xml, csv).
  * (OPTIONAL) The user shall be able to filter the results according to (name, DOB, phone, gender, or Email)
  
4. Writing unit tests is highly recommended and is a plus.

5. Your deliverable should include:
  * The source code.
  * ReadMe file for the IIS web application configuration (if needed).
  * Access DB file.
