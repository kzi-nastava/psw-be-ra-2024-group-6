INSERT INTO tours."Reviews"(
	"Id", "TouristId", "Rating", "Comment", "TourDate", "ReviewDate", "Images")
	VALUES (-1, -10, 1, 'I hated the tour!!!', TO_TIMESTAMP('16-10-2023 10:10:10', 'DD-MM-YYYY HH24:MI:SS'), TO_TIMESTAMP('16-10-2024 10:10:10', 'DD-MM-YYYY HH24:MI:SS'), '{{url1, url2}}');

INSERT INTO tours."Reviews"(
	"Id", "TouristId", "Rating", "Comment", "TourDate", "ReviewDate", "Images")
	VALUES (-2, -10, 3, 'The tour was okay, could have been longer.', TO_TIMESTAMP('16-10-2023 10:10:10', 'DD-MM-YYYY HH24:MI:SS'), TO_TIMESTAMP('16-10-2024 10:10:10', 'DD-MM-YYYY HH24:MI:SS'), '{{url}}');

INSERT INTO tours."Reviews"(
	"Id", "TouristId", "Rating", "Comment", "TourDate", "ReviewDate", "Images")
	VALUES (-3, -10, 5, 'Amazing tour, would recommend!!', TO_TIMESTAMP('16-10-2023 10:10:10', 'DD-MM-YYYY HH24:MI:SS'), TO_TIMESTAMP('16-10-2024 10:10:10', 'DD-MM-YYYY HH24:MI:SS'), '{{}}');