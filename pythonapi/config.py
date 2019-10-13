""""config file for pythonapi package"""

import os

APPLICATION_ROOT = '/'

PROGRAM_FOLDER = os.path.join(
    os.path.dirname(os.path.dirname(os.path.realpath(__file__))),
    'pythonapi', 'tests'
)

# path from api's __init__.py file
MAIN_PROGRAM = 'test0.py'
