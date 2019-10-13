"""REST API for execution"""
import os
import flask
import pythonapi


@pythonapi.app.route('/api/execution/start/', methods=["GET"])
def start_debug():

	# start program execution
	os.system(f"python {pythonapi.app.config['MAIN_PROGRAM']}")

	variables = pythonapi.api.info_locals()
	stack = pythonapi.api.info_stack()
	curr_line = pythonapi.api.get_current_line()

	# return program state
	program_state = {
    	"start": True,
		"current_line": curr_line,
		"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execution/end/', methods=["GET"])
def end_debug():

	# end program execution
	os.system('quit')

	# return program state
	program_state = {
		"successful_end": True
    }
	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execute/stepinto/', methods=["GET"])
def step_into():

	# step into line
	os.system('step')

	variables = pythonapi.api.info_locals()
	stack = pythonapi.api.info_stack()
	curr_line = pythonapi.api.get_current_line()

	# return new program state
	program_state = {
		"current_line": curr_line,
    	"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execute/stepover/', methods=["GET"])
def step_over():

	# step over line
	os.system('next')

	variables = pythonapi.api.info_locals()
	stack = pythonapi.api.info_stack()
	curr_line = pythonapi.api.get_current_line()

	# return new program state
	program_state= {
		"current_line": curr_line,
    	"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execute/continue/', methods=["GET"])
def continue_debug():

	# step over line
	os.system('continue')

	variables = pythonapi.api.info_locals()
	stack = pythonapi.api.info_stack()
	curr_line = pythonapi.api.get_current_line()

	# return new program state
	program_state= {
		"current_line": curr_line,
    	"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)


# TODO
@pythonapi.app.route('/api/execute/breakpoint/<int:line_number>/', methods=["GET"])
def set_breakpoint(line_number):

	# set breakpoint in pdb
	os.system(f'break {line_number}')

	program_state= {
		"added": True
	}

	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execute/breakpoint/<int:line_number>/', methods=["GET"])
def remove_breakpoint(line_number):

	# set breakpoint in pdb
	os.system(f'break {line_number}')

	program_state= {
		"added": True
	}

	return flask.jsonify(**program_state)
