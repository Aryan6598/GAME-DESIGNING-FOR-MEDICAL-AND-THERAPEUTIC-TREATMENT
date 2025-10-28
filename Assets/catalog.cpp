#include <iostream>
#include <vector>
#include <cmath>
#include <string>

// Helper function to decode value from a given base
long long decodeValue(const std::string& value, int base) {
    long long result = 0;
    for (char digit : value) {
        result *= base;
        if ('0' <= digit && digit <= '9') {
            result += digit - '0';
        } else if ('a' <= digit && digit <= 'f') { // For hex bases
            result += digit - 'a' + 10;
        }
    }
    return result;
}

// Lagrange interpolation to find the constant term (c)
long long lagrangeInterpolation(const std::vector<std::pair<int, long long>>& points) {
    long long result = 0;
    int n = points.size();
    
    for (int i = 0; i < n; ++i) {
        long long term = points[i].second;
        for (int j = 0; j < n; ++j) {
            if (i != j) {
                term *= points[j].first;
                term /= (points[j].first - points[i].first);
            }
        }
        result += term;
    }
    return result;
}

int main() {
    int n, k;
    
    // Read n and k (total number of points and minimum required points)
    std::cout << "Enter total number of points (n) and required points (k): ";
    std::cin >> n >> k;
    
    std::vector<std::pair<int, long long>> points;
    
    // Read each root (x, base, and encoded value)
    for (int i = 0; i < n; ++i) {
        int x, base;
        std::string value;
        
        std::cout << "Enter x, base, and encoded value for point " << i + 1 << ": ";
        std::cin >> x >> base >> value;
        
        // Decode the encoded y value
        long long y = decodeValue(value, base);
        points.push_back({x, y});
    }
    
    // Calculate the constant term using Lagrange interpolation
    long long constant_term = lagrangeInterpolation(points);
    std::cout << "Constant term (c): " << constant_term << std::endl;

    return 0;
}
