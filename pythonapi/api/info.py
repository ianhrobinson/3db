import os
import re
import pythonapi

def info_locals():
	'''
	returns all local variables with their values in a dictionary
	'''
	
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('locals()')

	return pythonapi.app.config['PROCESS'].before


def info_stack():
	'''
	returns stack frames, front of list = top
	'''

	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('where')

	filename = pythonapi.app.config['PROGRAM_NAME']
	unparsed_stack = pythonapi.app.config['PROCESS'].before
	# parsed_stack = []

	# temp = [unparsed_stack.split(filename)]

	# print(temp)
	# temp = temp[1]

	# temp = temp.split('->')[0]	

	# # TODO: parse stack better
	# parsed_stack = temp

	return unparsed_stack


def get_current_line():
	'''
	returns current line
	'''
	
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('u;;d;;l')

	line_num = -1

	# TODO: clean this up
	output = pythonapi.app.config['PROCESS'].before

	# temp = re.findall(r"/\[[0-9]+\]/*->", output)

	line_num = output

	return line_num