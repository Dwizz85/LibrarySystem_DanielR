Some SQL - Querys that was usefull in this project

- Ensure all BookAuthor relationships map to existing BookID and AuthorID

      CREATE PROCEDURE FindUnlinkedEntries
      AS
      BEGIN
        -- Select orphaned BookIDs
        SELECT ba.BookID 
        FROM BookAuthors ba 
        LEFT JOIN Books b ON ba.BookID = b.BookId
        WHERE b.BookId IS NULL;

        -- Select orphaned AuthorIDs
        SELECT ba.AuthorID 
        FROM BookAuthors ba 
        LEFT JOIN Authors a ON ba.AuthorID = a.AuthorId
        WHERE a.AuthorId IS NULL;
      END;

- Execute stored procedure:

      USE LibrarySystem;
      EXEC FindUnlinkedEntries;

- To reset the database (use with caution):

    DELETE FROM BookAuthors;
    DELETE FROM Books;
    DELETE FROM Authors;
    DBCC CHECKIDENT ('Books', RESEED, 0);  -- Reset the ID counter
    DBCC CHECKIDENT ('Authors', RESEED, 0);

- Simple but effective

    SELECT * FROM Books;
    SELECT * FROM Authors;
    SELECT * FROM BookAuthors;


                                                                          
                                                                      
                             ..:::::::..                              
                       .:-+#%%@@@@@@@@@%%*+-:.                        
                    .-*%@@@@@%###@@@@@%%%@@@@%+-.                     
                  :*%@@@%%%@@@@@@@@@@@@@%%%@%%@@%+:                   
                :#@@%%###@@@@@@@@@@@@@@@@@@@@%##%@@*:                 
              .*@@%%%%%@@@@@%@%%%%@@%@%%@%@@@@@@%%@@@+.               
             -%@@%%%@@%%@%@%%%%%%%%####%%%%%%@@@@@%#%@%:              
            =@@@#%@@%+**#%%%%%##********##%%%%%%@@@@%%@@-             
           =@@@#%@@@#******%%%*************#@%%%@%@@@##@@-            
          :@@@#%@@@%%#*******######%%%#%%#%%@%@%#*%@@%*%@@:           
         .#@@%#@@@@%%@%##*##*+===--:::-:--=*@@%%@%@%@@@#@@*           
         -@@@@@@@@%%@%%%%#=:..::-:  .:...  .=%%%%%%%@@@@@@@:          
         *@#%@##@@%%%%%%%# .=##*--..-.-*#+.  *%%%%%%%#=%@*+=          
        .#==%@+-#@%%%%%%%#.=#**#=...::##%%= =%%%%%%@%+*##*+=          
        .%@@==#@@%%%%%%%#. +**++*. ..-#=+%#.-@%%%%%%%@%==+@#          
         ##=#@++%%%%%%%@- .**#+-*+  .*#--%%: +@%%%%%%++%@#+=          
         =#@@@@%#@%%%%%%- .-:-====...+=-=+=. +%%%%%@%%@@@@@-          
         :@@@@@@@@%%%%%%#=:..... .#%:...  .:=%%%%%%%@@@%@@@:          
          *@@%#@@@@%@%%%%@%%##-..:=+-   +#%%@%%%%@%@@@@#@@+           
          .%@@##@@@@%@%%%%@++@=:..  ..:-#@*%%%%%%%@@@@#%@%.           
           :@@@#%@@@@%%%%%%-#=*+++-*=+%:*%+#%%%%%@@@@#%@%:            
            :%@%%%@@@@@%%%**#*#**#+%**%+#%+#%%@%@@@@#%@%:             
             .#@@%#@@@@@@@+*++%=%+%%+@#=%%-#@@@@@@#%@@*.              
               =%@@@%%@@@@#+==+=#=*#-*+:=:*@@@%#%%@@%-                
                .=%@@%#%%@@%%**+++==+-++**@@%##%@@%=.                 
                  .-#@@@#%@%%%%%##**#*###%@%%@@@*-.                   
                     .=*%@@@@##%%#*####%@@@@%*-.                      
                        .:-=**###%#%%##*+=-:.                         
                              -##%%##+-                               
                               :=+++-                                 
                                  ..            