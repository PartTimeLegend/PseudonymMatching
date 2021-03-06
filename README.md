Welcome to the Pseudonym Matching Windows Application.
======================================================

The problem was:
----------------

Data comes in that contains possible duplicates. The name Richard can also be Rich, Richie, Ricky, Richy, Rick, Dick and possibly a million others.

The solution:
-------------

Well I would normally have put all this in a Microsoft SQL Server database, but MSSQL 2012 was broken on my machine and I don't have time to rebuild it at the moment. It's falling apart, so the tape and elastic bands will have to hold a little longer.

You import the CSV you are sent that should be in the format

Name,Gender,Occurance

The occurance is ignored, as it holds no value for me.

Now you import the pseudonyms CSV in the format:

Basename,Pseudonyms

The pseudonyms are pipe seperated (|).

The pseudonyms can be changed in the Modify form, the original file is loaded into the datagridview and can be added to, altered and deleted. You can then save to a new CSV.

[![Build Status](https://travis-ci.org/PartTimeLegend/PseudonymMatching.png?branch=master)](https://travis-ci.org/PartTimeLegend/PseudonymMatching)

[![Flattr this git repo](http://api.flattr.com/button/flattr-badge-large.png)](https://flattr.com/submit/auto?user_id=parttimelegend&url=https://github.com/PartTimeLegend/PseudonymMatching&title=Pseudonym Matching&language=&tags=github&category=software) 
