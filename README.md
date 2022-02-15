# FinancialChat
Financial Chat SingnalR



0. create DataBase en SQLServer, and change the conectionString Inside appsettings.json from FinancialChat.Api (IdentityConnectionString)

    Data Source=localhost; Initial Catalog=FinancialSecurity; User Id=sa;Password=XXXXXXXXXXXXXXXXXXXX;

    Inside appsettings.json from FinancialChat.Api check bot url (ChatBot), Ip and Ports must be the same executed FinancialChatBot.Api

1. right click on root solution and properties, startup Project, select Single startup project, and select FinancialChat.Api

2. Make FinancialChat.Api default project. (right click set as startup project)

3. in Tools --> Nuget Packaga Manager --> Packaga manager console

4. in nuget console, Default project select FinancialChat.Identity

5. update-database -context FinancialChatIdentityDbContext

6. in nuget console, Default project select FinancialChat.Infrastructure

7. update-database -context FinancialChatDbContext

8. right click on root solution then properties, startup Project, select Multiple startup project, select FinancialChat.Api and FinancialChatBot.Api



In the Web application

1. install angular cli
$ npm install -g @angular/cli

2. inside path from the web aplicacion run: npm install

3. Chech urls and ports inside enviroments --> enviroment.ts

3. run web: ng serve --o
