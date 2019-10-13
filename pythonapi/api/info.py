import os
import pythonapi

def info_locals():
	'''
	returns all local variables with their values in a dictionary
	'''

	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('locals()')

	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	retval = pythonapi.app.config['PROCESS'].before

	pythonapi.app.config['PROCESS'].sendline('h')
	return retval;


def info_stack():
	'''
	returns stack frames, front of list = top
	'''

	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('where')

	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	filename = pythonapi.app.config['PROGRAM_NAME']
	unparsed_stack = pythonapi.app.config['PROCESS'].before

	pythonapi.app.config['PROCESS'].sendline('h')

	parsed_stack = []
	temp = unparsed_stack.split(filename)[1]
	temp = temp.split('->')[0][1:]

	temp = list(filter((lambda x: (x.isalpha())), temp))
	temp = ''.join(temp)

	parsed_stack = temp

	return parsed_stack


def get_current_line():
	'''
	returns current line
	'''
	
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('where')

	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	filename = pythonapi.app.config['PROGRAM_NAME']
	unparsed_stack = pythonapi.app.config['PROCESS'].before

	pythonapi.app.config['PROCESS'].sendline('h')

	parsed_stack = []
	temp = unparsed_stack.split(filename)[1]
	temp = temp.split('->')[0][1:]

	temp = list(filter((lambda x: (x.isnumeric())), temp))
	temp = ''.join(temp)

	parsed_stack = int(temp)

	return parsed_stack