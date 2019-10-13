import os
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

	unparsed_stack = pythonapi.app.config['PROCESS'].before
	parsed_stack = [unparsed_stack.split('->')]

	# TODO: parse stack better

	return parsed_stack


def get_current_line():
	'''
	returns current line
	'''
	
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('u;;d;;l')

	# TODO: clean this up

	# filename = pythonapi.app.config['PROGRAM_PATH']
	# output = pythonapi.app.config['PROCESS'].before

	# i = 0
	# while i < len(output):
	# 	if 	filename == output[i:len(filename)]:
	# 		i = i + len(filename) + 2
	# 		break
	# 	i += 1
	
	# # right at line number
	# s = ''
	# while output[i] != ')':
	# 	s.append(output[i])

	# line_no = int(s)

	return pythonapi.app.config['PROCESS'].before