# 3db
A 3D visual debugger for Python with VR support. Made for MHacks 12

## Getting Started

1. `git clone` this repo

2. `cd` into the newly cloned repo and install a python virtual environment using      
`$ python3 -m venv env`

3. activate the environment using      
	`$ source env/bin/activate`

4. Grab your favorite Python program and copy it into the `main/` folder. Be sure to include the line `import pdb;pdb.set_trace()` atop the file you wish to debug. Now, navigate to `config.py` and change `PROGRAM_NAME` to the name of the python file

5. run the api server using      
	`$ chmod +x bin/* `      
	`$ dos2unix bin/* `      
	`$ bin/pythonapirun`     

6. run the Unity program either by opening the Unity IDE and pressing play, OR running the windows executable
