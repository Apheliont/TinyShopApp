--detach full text index from stop list before recreating it once again
ALTER FULLTEXT INDEX ON Products
SET STOPLIST = OFF

DROP FULLTEXT STOPLIST ProductStoplist;

CREATE FULLTEXT STOPLIST ProductStoplist FROM SYSTEM STOPLIST;

ALTER FULLTEXT INDEX ON Products
SET STOPLIST = ProductStoplist