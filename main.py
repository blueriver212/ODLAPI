#import os
import sys
sys.path.append("C:\\Python39\\Lib")
import os
#file_name = sys.argv[1] + '.txt'
file_name = "test"
output_dir = os.path.join(os.getcwd(), file_name)
open(output_dir, 'a').close()