Hi)) This is an online store that provides easy access to the database. The administrator can receive, edit, add and delete data after authorization. And all this he performs on raw sql queries. In the event of an error, the system responds with a fairly clear explanation of the error.

Let's try to run it:вававава

1)
2)Double click on Tymchak_shop.csproj file
3)Open dbsettings.json and change ConnectionStrings:DefaultConnection
4)Open Package Manager Console:
  1.Add-Migration MyMigration -Context AppDBContent
  2.Update-DataBase
5)Now you can run. Started!!Huuh))
6)
