#!/usr/bin/python

# Ryan Wagner
# CS270
# Script2
# Renames any *.cxx files to *.cpp

import fnmatch
import os

currDir = os.listdir('.')
for filename in currDir:
	if(fnmatch.fnmatch(filename, '*.cxx')):
		os.rename(filename, (filename[0:filename.rfind('.')] + '.cpp'))
		print('Renamed %s to %s' % (filename, filename[0:filename.rfind('.')] + '.cpp'))
