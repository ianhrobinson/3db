"""REST API for likes."""
import flask
import pythonapi


@pythonapi.app.route('/api/v1/', methods=["GET"])
def get_likes():
  context = {
      "logname_likes_this": 1,
      "likes_count": 3,
      "url": flask.request.path,
  }
  return flask.jsonify(**context)
