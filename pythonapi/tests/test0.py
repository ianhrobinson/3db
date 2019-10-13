
def add_func(a, b):
	return a + b


if __name__ == "__main__":
	# test variable representations
	x = 9
	y = 3
	z = add_func(x, y)
	my_list = [x, y, z]
	my_dict = { 
		"x": x,
		"y": y,
		"z": z,
		"list": my_list
	}
	# test output console
	print(f'x: {x}')
	print(f'y: {y}')
	print(f'z: {z}')
	print(f'my_list: {my_list}')
	print(f'my_dict: {my_dict}')
