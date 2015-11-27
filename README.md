# IdeaMan
## Installation guide
After you have installed [Visual Studio](https://www.visualstudio.com/en-us/post-download-vs?sku=community&clcid=0x409), open the command prompt and run `sqllocaldb info`. 

It should print a name, like `v11.0` or `MSSQLLOCALDB`. 

If that does not happen, you have to [download and install LocalDB](https://drive.google.com/file/d/0B4RRc1RsYbpFQjZhQTdIX241VHM/view?usp=sharing). 

After that, rerun the command and confirm that the version printed corresponds to the one in `Web.config`. 

`<add name="DefaultConnection" connectionString="Data Source=(LocalDb)\v11.0; ... />` 

After that following the next order in Visual Studio:
 - Build -> Build Solution
 - In the upper right, type `nuget console` to open the *Packet Manager Console*
 - In the package manager console type `Update-Database`
 - You are good to go!
