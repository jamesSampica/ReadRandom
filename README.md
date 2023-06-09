# ReadRandom
Dotnet library for reading random numbers from a CPU's digital random number generator (DRNG).

This library contains methods for invoking the `RDRAND` and `RDSEED` instructions on Intel and AMD CPUs.

See https://www.intel.com/content/www/us/en/developer/articles/guide/intel-digital-random-number-generator-drng-software-implementation-guide.html for more details

![Digital Random Number Generator design](drng.jpg "Digital Random Number Generator design")

# Limitations

- Does not poll the ECX register to determine if RDRAND and RDSEED are supported processor features
- Does not support the REX a prefix and can only operate in 32 bit mode. 