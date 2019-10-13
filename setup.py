"""
pythonapi package configuration.

Zach Boyle <zboyle@umich.edu>
Marcelo Almeida <mgba@umich.edu>
Nick Kroetsch <kroetsch@umich.edu>
Ian Robinson <ihr@umich.edu>
"""

from setuptools import setup

setup(
    name='pythonapi',
    version='0.0.1',
    packages=['pythonapi'],
    include_package_data=True,
    install_requires=[
        'Flask==1.1.1',
        'Flask-Testing==0.7.1',
        'pycodestyle==2.5.0',
        'pydocstyle==4.0.1',
        'pylint==2.4.1',
        'pytest==5.2.0',
        'requests==2.22.0',
        'sh==1.12.14',
        'pexpect==4.7'
    ],
)
