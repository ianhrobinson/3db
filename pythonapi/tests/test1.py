import subprocess

pid = subprocess.Popen(["python", "-m", "pdb", "test0.py", "&"]).pid

print(pid)
