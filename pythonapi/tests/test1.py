import pexpect

child = pexpect.spawnu('python test0.py')
print('spawned thread')
child.expect('(Pdb)')
print('first expect')
child.sendline('l')
print(f'child after: {child.before}')
child.expect('(Pdb)')
print('after second expect')
child.sendline('l')
print(f' child after2: {child.before}')
child.expect('(Pdb)')
child.sendline('l')
print(child.before)