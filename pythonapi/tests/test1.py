import pexpect

child = pexpect.spawnu('python test0.py')
print('spawned thread')
child.expect('(Pdb)')
child.sendline('locals()')
print(f'first response: {child.after}')
child.expect('(Pdb)')
child.sendline('b 12')
print(f'second response: {child.before}')
child.expect('(Pdb)')
child.sendline('c')
print(f'third one:{child.before}')