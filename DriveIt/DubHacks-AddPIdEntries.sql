INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('04', 'Calculated engine load', '%', 0, 100);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('05', 'Engine coolant temperature', ' Celcius', -40, 215);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('0A', 'Fuel pressure', 'kPa', 0, 765);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('0C', 'Engine RPM', 'rpm', 0, 16383.75);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('0D', 'Vehicle speed', 'km/h', 0, 255);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('1F', 'Run time since engine start', 'seconds', 0, 65535);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('21', 'Distance traveled with malfunction indicator lamp (MIL) on', 'km', 0, 65535);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('23', 'Fuel Rail Gauge Pressure', 'kPa', 0, 655350);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('2F', 'Fuel Tank Level Input', '%', 0, 100);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('52', 'Ethanol fuel %', '%', 0, 100);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('5B', 'Hybrid battery pack remaining life', '%', 0, 100);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('5C', 'Engine oil temperature', 'Celcius', -40, 210);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('5E', 'Engine fuel rate', 'L/h', 0, 3276.75);

INSERT INTO dbo.PidTable (PId, Descriptions, Units, MinValue, MaxValue)
VALUES ('61', 'Driver''s demand engine - percent torque', '%', -125, 125);