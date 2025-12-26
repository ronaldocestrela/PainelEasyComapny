from datetime import datetime
from pathlib import Path

from flask import Flask, jsonify, request


app = Flask(__name__)
STORAGE_DIR = Path("postbacks")


def ensure_storage_dir() -> None:
	"""Create the storage directory once per process."""
	STORAGE_DIR.mkdir(parents=True, exist_ok=True)


def detect_payload_type(content_type: str) -> str:
	"""Return a friendly label for the incoming payload."""
	if not content_type:
		return "desconhecido"

	lowered = content_type.split(";")[0].strip().lower()
	if lowered == "application/json":
		return "json"
	if lowered in {"application/x-www-form-urlencoded", "multipart/form-data"}:
		return "form"
	if lowered.startswith("text/"):
		return "texto"
	return lowered


def build_record(payload_bytes: bytes, content_type: str) -> str:
	"""Compose the record to be stored in the .txt file."""
	timestamp = datetime.utcnow().isoformat() + "Z"
	payload_str = payload_bytes.decode("utf-8", errors="replace")
	data_type = detect_payload_type(content_type)

	return (
		f"timestamp: {timestamp}\n"
		f"content_type: {content_type or 'não informado'}\n"
		f"data_type: {data_type}\n"
		"payload:\n"
		f"{payload_str}\n"
	)


@app.route("/postback-registro", methods=["POST"])
def handle_postback_registro():
	ensure_storage_dir()

	raw_payload = request.get_data()
	content_type = request.headers.get("Content-Type", "")
	record = build_record(raw_payload, content_type)

	filename = datetime.utcnow().strftime("registro_postback_%Y%m%dT%H%M%S%fZ.txt")
	filepath = STORAGE_DIR / filename
	filepath.write_text(record, encoding="utf-8")

	return jsonify({
		"message": "postback registrado",
		"file": str(filepath),
		"data_type": detect_payload_type(content_type),
	})

@app.route("/postback-ftd", methods=["POST"])
def handle_postback_ftd():
	ensure_storage_dir()

	raw_payload = request.get_data()
	content_type = request.headers.get("Content-Type", "")
	record = build_record(raw_payload, content_type)

	filename = datetime.utcnow().strftime("ftd_postback_%Y%m%dT%H%M%S%fZ.txt")
	filepath = STORAGE_DIR / filename
	filepath.write_text(record, encoding="utf-8")

	return jsonify({
		"message": "postback registrado",
		"file": str(filepath),
		"data_type": detect_payload_type(content_type),
	})


@app.route("/health", methods=["GET"])
def health():
	return jsonify({"status": "ok"})


if __name__ == "__main__":
	ensure_storage_dir()
	app.run(host="0.0.0.0", port=8000, debug=True)
