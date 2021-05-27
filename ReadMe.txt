1. A SQL localdb instance is used for this project.  
If needed use the Package Manager Console to perform the enable-migrations, update-database as needed to create the DB.

2. Have SQL Server Management Studio installed to make this step 'easier.' :)
The SQL script below can be used to populate the DB once the migrations have created the DB. Substitute in the three VALUES fields ([Email],
[Password Hash from the HashPassword program], and [a Username of you choice]), all other fields can remain the same.
Execute the script in SQL Server Management Studio.

INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp],   
[PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled],   
[AccessFailedCount], [UserName])   
VALUES (N'9f15bdd0fcd5423190c2e877ba0228ee', N'abc@gail.com', 1,   
N'ALkHGax/i5KBYWJ7q4jhJmMKmm2quBtnnqS8KcmLWd2kQpN6FaGVulDmmX12s7YAyQ==',   
N'a7bc5c5c-6169-4911-b935-6fc4df01d313', NULL, 0, 0, NULL, 0, 0, N'Jignesh')  

If a password hash is needed, change the line "var HashedPWD = pHasher.HashPassword("123456");" to have the desired password.
The password has 


3. First run JukkaServer20OWINAuth.  The "HTTP Error 403.14 - Forbidden" can be ignored.
Then run JukkOWINTest.  If successful the token and 'API response' will be display in the console window as show below but with 
a different Token value:

Token issued is: 0j_5-MM13jZcDkU3PrkLf9eYW6NSqpTGYlxdr_jJdHkuK84NJuaMPaptj_Q3NoGXWpAqhvwMSwzxMgAMA9CGV_5SbVgJ83vZ5QjNpPv7aP7B96TkL67_1ETb08hbiKl4I0cwkgnkVshS3J3555o_CcQFD4z5afKPhTBnDOy5cnOD9wJAKzkSeb8AHWOtfnoCKpzpY8LAVzCYil7xLQpeu4KhmEy2cauX5smEscjMfAw8GTT80ViL1g1A-N3d9N9oQWme3tWOv57rPWw2TKN8NT2UWdYjafM0anksDAbg7N9-KyhWXV4ov-9AudADQgmymdPVGyao-3np8n1sRwQDxNsA2q6bA2u0ZBGYtK1ZfI7JbCH1TPrqENMTMiNYBc5eAqrYHOnPD9V6Rv8fJPLmSLCNuIx4rLkh6P1ABh8otdeaWRtLdo9DsCl1HhXBaYhZhvg94ISQZBpiF9VfKswApSwTjnoiY6i5zpcvD5Pe9Ro

Successfully receive API response
API responese : "Hello, From the Jukka Cloud Service. "

ORRRR.... Better yet...

4. In the solution property pages 
	select Common Properties -> Startup Project
	select button "Multiple startup projects
	for JukkaOWINTest and JukkaServer20OWINAuth project change action from None to "Start" or "Start without debugging"
	select Apply and OK.
	
5. Hit Start to start the fun.s
