# MeetingsOrganizer Console App

Functionality:
All commands are described in a program.
1. Login form.
    - Enter name and surname. Program checks if there is person with this name in personsData.json (this file and meetingsData.json file is checked and created if doesn't exist) file. If there is no such person, then creates new person with inputed values and pushes new data to data file.
    - Exit. Exits program.
2. Main menu.
    - Create new meeting. User inputs asked data. Program automaticaly assigns logged in person as a meeting host. After entering all data correctly program updates list of all meetings with all meetings data into a meetingsData.json and returns user to main menu.
    - Select. Brings up a select meetings form where user can choose meeting to manage. After selecting a meeting user is prompted to selected meeting menu.
    - Logout. Brings back user to login form.
    - Getall + filtering properties. Shows existing lists in meetingsData.json file. If filtering parameters are entered then it shows only those meetings which matches specified parameters.
3. Selected meeting menu.
    - Delete meeting. Removes meeting from json file, removes this meeting form all people schedules.
    - aPerson. Add person to a meeting. If entered person doesn't exist - program creates that person and updates personsData json file. This meetign is added to this person schedule.
    - rPerson. Select a person from a meeting and remove that person from it. Update data files.
    - exit. Returns user to select meeting menu.
