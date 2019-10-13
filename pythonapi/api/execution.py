"""REST API for execution"""
import pexpect
import flask
import pythonapi


@pythonapi.app.route('/api/execution/start/', methods=["GET"])
def start_debug():

	# start program execution
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('continue')

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
	pythonapi.app.config['PROCESS'].kill()

	# return program state
	program_state = {
		"successful_end": True
    }
	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execution/stepinto/', methods=["GET"])
def step_into():

	# step into line
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('step')

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


@pythonapi.app.route('/api/execution/stepover/', methods=["GET"])
def step_over():

	# step over line
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('next')

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


@pythonapi.app.route('/api/execution/continue/', methods=["GET"])
def continue_debug():

	# step over line
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline('continue')

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
@pythonapi.app.route('/api/execution/set_breakpoint/<int:line_number>/', methods=["GET"])
def set_breakpoint(line_number):

	# set breakpoint in pdb
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline(f'break {line_number}')

	program_state= {
		"added": True
	}

	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execution/remove_breakpoint/<int:line_number>/', methods=["GET"])
def remove_breakpoint(line_number):

	# set breakpoint in pdb
	pythonapi.app.config['PROCESS'].expect('(Pdb)')
	pythonapi.app.config['PROCESS'].sendline(f'clear {line_number}')

	program_state= {
		"added": True
	}

	return flask.jsonify(**program_state)
