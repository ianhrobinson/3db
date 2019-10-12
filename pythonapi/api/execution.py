"""REST API for execution"""
import flask
import pythonapi
import pythonapi.api.info as info


@pythonapi.app.route('/api/execution/start/', methods=["POST"])
def start_debug():
	
	# start program execution
	variables = info.info_locals()
	stack = info.info_stack()

	# return program state
	program_state = {
    	"start": True,
		"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execution/end/', methods=["POST"])
def end_debug():
	
	# end program execution

	# return program state
    program_state = {
      "successful_end": True
    }
    return flask.jsonify(**program_state)


@pythonapi.app.route('/api/execute/stepinto/', methods=["POST"])
def step_into():

	# step into line
	variables = info.info_locals()
	stack = info.info_stack()

	# return new program state
	program_state = {
    	"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)

@pythonapi.app.route('/api/execute/stepover/', methods=["POST"])
def step_over():

	# step over line
	variables = info.info_locals()
	stack = info.info_stack()

	# return new program state
	program_state= {
    	"variables": variables,
		"stack": stack
	}
	return flask.jsonify(**program_state)
