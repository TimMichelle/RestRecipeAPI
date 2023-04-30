#!/bin/bash

set -e

host="$1"
port="$2"
cmd="$3"

until nc -z "$host" "$port"; do
  >&2 echo "Waiting for PostgreSQL to become available..."
  sleep 1
done

echo $cmd
>&2 echo "PostgreSQL is now available"
exec $cmd
