import subprocess
from subprocess import PIPE
import sys

process = subprocess.Popen(["python", "test0.py"], stdout=PIPE, stdin=PIPE, universal_newlines=True, shell=False)

# process.stdin.write("l\n".encode())

try:
	stdout, stderr = process.communicate(input='l')
	print(stdout)
except ValueError as e:
	print(e)
	process.kill()
	print('killed process')

