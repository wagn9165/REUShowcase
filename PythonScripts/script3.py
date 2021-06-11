#!/usr/bin/python

# Ryan Wagner
# CS270
# Script3
# Copies arguments into .backup folder

import sys
import shutil
import os

currpath = os.getcwd()

targetpath = currpath + "/.backup"

#Check if .backup exists
if(not(os.path.isdir(targetpath))):
	os.mkdir(targetpath)
	print("Made .backup folder")

if(not(len(sys.argv) > 1)):
	print("Script3 requires arguments to backup.")

count = 0
for files in sys.argv:
	if(not(count == 0)): #Skips ./script3.py
		currfile = currpath + "/" + files

		#Check file exists
		if(not(os.access(currfile, os.F_OK))):
			print files + " does not exist in current directory"
			continue

		#File destination
		workingpath = targetpath + '/' + files

		#Checks if files already exists in .backup
		if(os.access(workingpath, os.F_OK)):
			inputRead = str(raw_input("File already exists, do you want to override? (y/n) "))
			if(inputRead == "n"):
				continue
			elif not(inputRead == "y"):
				print("Unaccepted input, skipping copy")
				continue
			else:
				flagRemove = 1 #If trying to copy dir

		#If is a file
		if(os.path.isfile(currfile)):
			shutil.copy2(currfile, workingpath)
			print(files + "->" + workingpath)

		#Else if is a directory
		elif(os.path.isdir(currfile)):
			if(flagRemove): #Crashed if dir did not already exist, so
					#I made this flag
				shutil.rmtree(workingpath)
			shutil.copytree(currfile, workingpath)
			print(files + "->" + workingpath)

		#Should not occur
		else:
			print("Could not copy file")
	count += 1
	flagRemove = 0

print("End")
