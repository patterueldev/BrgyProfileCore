IF EXIST "%USERPROFILE%\Documents\BrgyProfileCore" (
    ren "%USERPROFILE%\Documents\BrgyProfileCore" "BrgyProfileCore-%date:/=-% %time::=-%"
)
mkdir "%USERPROFILE%\Documents\BrgyProfileCore"
copy blank-app.db "%USERPROFILE%\Documents\BrgyProfileCore\app.db"