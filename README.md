# Brgy Profile

## How to start development
1. Check git installation (open command prompt or powershell, type `git` and press enter and check the response.) If git is not installed, download from https://git-scm.com/downloads (for Windows only).
2. Once git is confirmed to be installed, perform a clone using this url: `https://github.com/jpteruel095/BrgyProfileCore.git`.
    * Open a command line application (cmd or powershell, or git-scm's git bash)
    * Navigate to a directory (e.g., `cd C:\projects`)
    * Perform a git clone: `git clone https://github.com/jpteruel095/BrgyProfileCore.git`
    * Navigate to the repository folder: `cd BrgyProfileCore`
3. To check updates, you have to keep your local copy updated.
    * Identify where you cloned the repository
    * Navigate to that folder (e.g., `cd C:\projects\BrgyProfileCore`)
    * Fetch tracked changes from git server: `git fetch origin`
    * Stash local changes (unless you're a developer): `git stash -u`
    * Pull the changes: `git pull origin main`(It should be main branch if #2 was followed.)
4. Using powershell.exe, go to the repository root folder (e.g., `cd C:\projects\BrgyProfileCore`) and run the command `initialize-db.bat`
    * This will copy a blank database to the documents folder
    * Alternatively, you could navigate using the File Explorer, then double-clicking the `initialize-db.bat` file.
5. Open `BrgyProfileCore.sln` using Visual Studio (as of writing, `Visual Studio 2019 Community Edition`).
6. Go to `Tools > NuGet Package Manager > Package Manager Console`. This should open a command line console on the bottom of the IDE and you should see a shell: `PM> |`
7. On the console, type `Update-Database` and press enter. You should see something like this:
```
PM> Update-Database
Build started...
Build succeeded.
No migrations were applied. The database is already up to date.
Done.
PM> 
```
8. If no error occurs, you should be able to run the app by pressing F5 or clicking the "Start" button on the top, with a green Play icon.