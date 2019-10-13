import os
import pexpect
import flask
import pythonapi


@pythonapi.app.route('/api/initialize/', methods=["GET"])
def initialize():

	# start program execution
	pythonapi.app.config['PROCESS'] = pexpect.spawnu(
		f'python {pythonapi.app.config["PROGRAM_PATH"]}'
	)

	f = open(pythonapi.app.config['PROGRAM_PATH'], 'r')
	# create list of lines
	code = f.read().split('\n')
	f.close()

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