#!/usr/bin/python

# Ryan Wagner
# CS270
# Script4
# Reverses the contents of a text file

import sys
import os
import shutil

#Input Validation
if(not(len(sys.argv) > 1)):
	print("Script4 requires an argument to read from.")
	exit()
if(not(len(sys.argv) == 2)):
	print("Script4 can only read from one file at a time.")
	exit()

currentpath = os.getcwd()

if(not(os.path.isfile(currentpath+"/"+sys.argv[1]))):
	print("Unable to open file. Either file is directory or does not exist.")
	exit()

#Open files
inputFile = open(sys.argv[1], "r")
outputFile = open("temp.txt", "w") #Used as buffer

#Loop that reverses line by line and writes to buffer file
#Breakdown:
#	reversed(line): reverses the line and returns an iterator
#	list(reversed(line)): turns the iterator into a list
#	''.join(list(reversed(line))): implodes the list into single string
#	(''.join(list(reversed(line)))).lstrip('\n'): strips newline char after
#		having read in line from original
for line in inputFile:
	outputFile.write((''.join(list(reversed(line)))).lstrip('\n') + '\n')

#Close files
inputFile.close()
outputFile.close()

#Copy buffer over original
shutil.copy2(currentpath + "/temp.txt", currentpath + "/" + sys.argv[1])

#Delete buffer
os.remove(currentpath + "/temp.txt")
print("File reversed.")
