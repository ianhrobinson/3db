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

	vars = retval.split("from '/usr/lib/python3.6/pdb.py'>,",1)[1]
	vars = vars.rsplit("}\r\n(",1)[0]
	vars = "{" + vars + "}"

	return vars


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
	print(f'unparsed: {unparsed_stack}')
	temp = unparsed_stack.split('->')[-2]
	print(f'temp: {temp}')
	temp = temp.split(filename)[-1]

	temp = list(filter((lambda x: (x.isnumeric())), temp))
	temp = ''.join(temp)

	parsed_stack = int(temp)

	return parsed_stack