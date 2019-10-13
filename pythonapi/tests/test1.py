import subprocess

process = subprocess.Popen(["python", "-m", "pdb", "test0.py"], shell=True)

try:
	stdout, stderr = process.communicate(input='l', timeout=3)
	print(stdout)
except:
	process.kill()

