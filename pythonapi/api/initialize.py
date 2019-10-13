import os
import flask
import pythonapi


@pythonapi.app.route('/api/initialize/', methods=["GET"])
def initialize():

	code = {}
	i = 1
	# start program execution
	
	with open(pythonapi.app.config['PROGRAM_PATH'], 'r') as f:
		# ignore newline character
		line = f.readline()
		while line:
			code[i] = line
			line = f.readline()
			i += 1

	variables = pythonapi.api.info_locals()
	stack = pythonapi.api.info_stack()
	curr_line = pythonapi.api.get_current_line()

	# return program state
	program_state = {
    	"start": True,
		"code": code,
		"current_line": curr_line,
		"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)