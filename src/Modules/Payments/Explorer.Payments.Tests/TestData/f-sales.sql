INSERT INTO payments."Sales"(
    "Id", "TourId", "StartDate", "EndDate", "SalePercentage")
    VALUES (-1, -2, TO_TIMESTAMP('16-12-2024 10:10:10', 'DD-MM-YYYY HH24:MI:SS') AT TIME ZONE 'UTC', TO_TIMESTAMP('16-01-2025 10:10:10', 'DD-MM-YYYY HH24:MI:SS') AT TIME ZONE 'UTC', 12);