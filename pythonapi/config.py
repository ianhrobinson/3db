""""config file for pythonapi package"""

import os

APPLICATION_ROOT = '/'

PROGRAM_FOLDER = os.path.join(
    os.path.dirname(os.path.dirname(os.path.realpath(__file__))),
    'pythonapi', 'tests'
)

PROGRAM_NAME = 'test0.py'

PROGRAM_PATH = os.path.join(
	PROGRAM_FOLDER, 
	PROGRAM_NAME
)