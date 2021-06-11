#!/usr/bin/python

# Ryan Wagner
# CS270
# Script1
# Either adds or removes line numbers. Does the opposite of what the source file has

import sys

if (not(len(sys.argv) > 1)):
	print ('Script1 requires an argument to read from.')
	exit()
elif(not(len(sys.argv) == 2)):
	print ("Script1 can only read from one file at a time. Please supply one argument.")
	exit()

#print 'Number of arguments:', len(sys.argv), 'arguments.'
#print 'Argument List:', str(sys.argv)

inputFile = open(sys.argv[1], "r")

if (inputFile.read(1)).isdigit():
	inputFile.seek(0)
	for line in inputFile:
		print (line.strip("0123456789")).strip()
	
else:
	inputFile.seek(0)

	count = 1

	for line in inputFile:
		print '0' + str(count) + ' ' + line.rstrip()
		count += 1



