import subprocess

pid = subprocess.Popen(["python", "test0.py", "&"]).pid

print(pid)
