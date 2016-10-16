SELECT h1.PId, PidTable.Descriptions, PidTable.Units, Users.FirstName, Users.LastName, Users.Age, Cars.Make, Cars.Model, Cars.Year, Cars.IsHybrid, AVG(h1.Value) AS Value,
	(SELECT AVG(h2.Value) AS NationalValue 
	FROM Histories h2 
	WHERE h1.PId = h2.PId AND h2.CarId = 1
	GROUP BY h2.PId) AS NationalValue
FROM Histories h1, PidTable, Cars, Users 
WHERE PidTable.PId = h1.PId AND h1.UserId = Users.UserId AND h1.UserId = 1 AND h1.CarId = 5 
GROUP BY h1.PId, PidTable.Descriptions, PidTable.Units, Users.FirstName, Users.LastName, Users.Age, Cars.Make, Cars.Model, Cars.Year, Cars.IsHybrid;