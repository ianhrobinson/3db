import os
import pythonapi

def info_locals():
	'''
	returns all local variables with their values in a dictionary
	'''
	
	variables = os.system('locals()')

	return None


def info_stack():
	'''
	returns stack frames, front of list = top
	'''
	return None


def get_current_line():
	'''
	returns current line
	'''
	filename = pythonapi.api.config['PROGRAM_NAME']
	output = stderr.PIPE("u;;d;;l")
	i = 0
	while i < len(output):
		if 	filename == output[i:len(filename)]
			i = i + len(filename) + 2
			break
		i += 1
	
	# right at line number
	s = ''
	while output[i] != ')':
		s.append(output[i])

	line_no = int(s)

	return line_no