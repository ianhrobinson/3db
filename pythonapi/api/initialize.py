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

	# return program state
	program_state = {
		"code": code
	}
	return flask.jsonify(**program_state)