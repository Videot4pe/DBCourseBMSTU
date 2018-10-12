import pandas as pd
import random
import string

def rand_string():
	strs = ""
	strs = strs + random.choice(string.ascii_uppercase)
	for i in range(random.randint(3, 7)):
		strs = strs + random.choice(string.ascii_lowercase)
	return strs

def rand_genre():
	strs = ['Rock', 'Metal', 'Hard rock', 'Classic', 'Electronic']
	return strs[random.randint(0, 4)]

def rand_date():
	strs = "19"
	strs = strs + str(random.randint(68, 99))
	strs = strs + "-"
	a = random.randint(1, 12)
	if a > 9:
		strs = strs + str(a)
	else:
		strs = strs + "0" + str(a)
	strs = strs + "-"
	a = random.randint(1, 30)
	if a > 9:
		strs = strs + str(a)
	else:
		strs = strs + "0" + str(a)
	return strs

strs = ["" for x in range(400)]
strsur = ["" for x in range(400)]
strsbirth = ["" for x in range(400)]
strsinst = ["" for x in range(400)]

for j in range(1, 400):
	strs[j] = rand_string()
	strsur[j] = rand_string()
	strsbirth[j] = rand_date()
	strsinst[j] = rand_string()
	
raw_data = {'Name': strs, 
        'Surname': strsur, 
        'BirthDate': strsbirth, 
        'Instrument': strsinst}
df = pd.DataFrame(raw_data, columns = ['Name', 'Surname', 'BirthDate', 'Instrument'])
df.to_csv('musicians.csv')


strt = ["" for x in range(600)]
strdate = ["" for x in range(600)]
strgroup = ["" for x in range(600)]
strgenre = ["" for x in range(600)]
strcountry = ["" for x in range(600)]
strduration = ["" for x in range(600)]
strtop = ["" for x in range(600)]

for j in range(1, 600):
	strt[j] = rand_string()
	strdate[j] = rand_date()
	strgroup[j] = random.randint(0, 150)
	strgenre[j] = rand_genre()
	strcountry[j] = rand_string()
	strduration[j] = random.randint(25, 100)
	strtop[j] = random.randint(1, 100)

raw_data = {'Title': strt, 
        'DateOfRecord': strdate, 
        'GroupID': strgroup, 
        'Genre': strgenre,
	'Country': strcountry,
	'Duration': strduration,
	'TopOfTheYear': strtop}
df = pd.DataFrame(raw_data, columns = ['Title', 'DateOfRecord', 'GroupID', 'Genre', 'Country', 'Duration', 'TopOfTheYear'])
df.to_csv('albums.csv')

strdate = ["" for x in range(150)]
strmem = ["" for x in range(150)]
stramount = ["" for x in range(150)]
strg = ["" for x in range(150)]
for j in range(1, 150):
	strdate[j] = rand_date()
	strmem[j] = random.randint(2, 6)
	stramount[j] = random.randint(3, 15)
	strg[j] = rand_string()
        
raw_data = {'Name': strg, 
        'DateOfCreation': strdate, 
        'NumberOfMembers': strmem, 
        'AlbumsAmount': stramount}
df = pd.DataFrame(raw_data, columns = ['Name', 'DateOfCreation', 'NumberOfMembers', 'AlbumsAmount'])
df.to_csv('group.csv')

strid = ["" for x in range(400)]
strgroup = ["" for x in range(400)]

for j in range(1, 400):
	strid[j] = j
	strgroup[j] = random.randint(0, 150)
	
raw_data = {'GroupId': strgroup}
df = pd.DataFrame(raw_data, columns = ['GroupId'])
df.to_csv('musiciansAndGroups.csv')
