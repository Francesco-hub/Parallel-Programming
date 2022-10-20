import torch
import time

if torch.cuda.is_available():
    device = torch.device("cuda")
else:
    device = torch.device("cpu")

print("using", device, "device")

matrix_size = 10000

x = torch.randn(matrix_size, matrix_size)
y = torch.randn(matrix_size, matrix_size)

'''print("************ CPU SPEED ***************")
start = time.time()
result = torch.matmul(x,y)
print(time.time() - start)
print("verify device:", result.device)'''


x_gpu = x.to(device)
y_gpu = y.to(device)
torch.cuda.synchronize()


print("------------ GPU SPEED ------------")
start = time.time()
result_gpu = torch.matmul(x_gpu,y_gpu)
torch.cuda.synchronize()
print("Matrix size: " + str(matrix_size))
print(str(time.time() - start) + "  Seconds")
#print("verify device:", result_gpu.device)