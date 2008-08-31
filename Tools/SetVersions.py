# The MIT License
#
# Copyright (c) 2008 Kimmo Varis <kimmov@winmerge.org>
# 
# Permission is hereby granted, free of charge, to any person obtaining a copy
# of this software and associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
# copies of the Software, and to permit persons to whom the Software is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice and this permission notice shall be included in
# all copies or substantial portions of the Software.
#
# THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
# IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
# LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
# OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
# THE SOFTWARE.

# $Id$

# SetVersions.py
# A Python script to set various component's version numbers in the project.

import ConfigParser
import getopt
import os
import shutil
import sys

def process_NSIS(filename, config, sect):
  '''Process NSIS section in the ini file.'''

  ver = config.get(sect, 'version')
  file = config.get(sect, 'path')
  desc = config.get(sect, 'description')
  
  print '%s : %s' % (sect, desc)
  print '  File: ' + file
  print '  Version: ' + ver
  
  inidir = os.path.dirname(filename)
  nsisfile = os.path.join(inidir, file)
  
  ret = set_NSIS_ver(nsisfile, ver)
  return ret
  
def set_NSIS_ver(file, version):
  '''Set version into NSIS installer file.'''

  outfile = file + '.bak'
  try:
    fread = open(file, 'r')
  except IOError, (errno, strerror):
    print 'Cannot open file ' + file + ' for reading'
    print 'Error: ' + strerror
    return False

  try:
    fwrite = open(outfile, 'w')
  except IOError, (errno, strerror):
    print 'Cannot open file ' + infile + ' for writing'
    print 'Error: ' + strerror
    fread.close()
    return False

  # Replace PRODUCT_ VERSION macro value with new value
  for line in fread:
    if line.startswith('!define PRODUCT_VERSION'):
	  ind = line.find('\"')
	  ind2 = line.rfind('\"')
	  if ind != -1 and ind2 != -1:
	    line = line[:ind] + version + line[ind2 + 1:]
    fwrite.write(line)
	
  fread.close()
  fwrite.close()
  
  shutil.move(outfile, file)
  
  return True

def process_versions(filename):
  '''Process all sections found from the given ini file.'''

  config = ConfigParser.ConfigParser()
  config.readfp(open(filename))

  ret = False
  sectlist = config.sections()
  print 'Setting versions:'
  for sect in sectlist:
    vertype = config.get(sect, 'type')
    if vertype == 'NSIS':
      ret = process_NSIS(filename, config, sect)
  return ret
	  
def usage():
  print 'Script to set program component version numbers.'
  print 'Usage: SetVersions [filename]'

def main(argv):
  filename = ''
  createbaks = True
  if len(argv) > 0:
    opts, args = getopt.getopt(argv, 'h', ['help'])

    for opt, arg in opts:
      if opt in ('-h', '--help'):
        usage()
        sys.exit()

    if len(args) == 1:
      filename = args[0]
    else:
      usage()
      sys.exit()
  else:
    usage()
    sys.exit()
  
  filename = os.path.abspath(filename)
  if os.path.exists(filename):
    ret = process_versions(filename)
    if ret == True:
      print 'Version numbers set successfully!'
  else:
    print 'ERROR: Could not find file: ' + filename

# MAIN #
if __name__ == "__main__":
    main(sys.argv[1:])
