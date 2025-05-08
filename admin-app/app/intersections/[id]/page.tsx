"use client";

import React, {useEffect, useState} from "react";
import {useRouter, useParams} from "next/navigation";
import {intersectionApi} from "@/lib/api";
import {Intersection} from "@/lib/api-types";
import {Button} from "@/components/ui/button";
import {Input} from "@/components/ui/input";
import {Label} from "@/components/ui/label";
import {PageLoading} from "@/components/layout/page-loading";

type FormState = {
    city: string;
    location: string;
    isActive: boolean;
};

export default function IntersectionDetailsPage() {
    const params = useParams();
    const router = useRouter();
    const idParam = params.id;
    const isNew = idParam === "new";

    const [form, setForm] = useState<FormState>({
        city: "",
        location: "",
        isActive: true,
    });
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!isNew) {
            setLoading(true);
            // Fetch the current intersection details for update
            intersectionApi
                .getById(Number(idParam))
                .then((response) => {
                    const data: Intersection = response.result;
                    setForm({
                        city: data.city ?? "",
                        location: data.location ?? "",
                        isActive: data.isActive,
                    });
                })
                .catch(() => {
                    setError("Failed to load intersection details");
                })
                .finally(() => setLoading(false));
        }
    }, [idParam, isNew]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const {name, value} = e.target;
        setForm((prev) => ({...prev, [name]: value}));
    };

    const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const {name, checked} = e.target;
        setForm((prev) => ({...prev, [name]: checked}));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        try {
            if (isNew) {
                // Create new intersection
                await intersectionApi.create(form);
            } else {
                // Update existing intersection
                await intersectionApi.update(Number(idParam), form);
            }
            router.push("/intersections");
        } catch {
            setError("Failed to save intersection");
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <PageLoading/>;

    return (
        <div className="max-w-xl mx-auto p-4">
            <h1 className="text-2xl font-bold mb-4">
                {isNew ? "Create Intersection" : "Edit Intersection"}
            </h1>
            {error && <p className="mb-4 text-red-500">{error}</p>}
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <Label htmlFor="city">City</Label>
                    <Input
                        id="city"
                        name="city"
                        value={form.city}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <Label htmlFor="location">Location</Label>
                    <Input
                        id="location"
                        name="location"
                        value={form.location}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div className="flex items-center">
                    <input
                        type="checkbox"
                        id="isActive"
                        name="isActive"
                        checked={form.isActive}
                        onChange={handleCheckboxChange}
                        className="mr-2"
                    />
                    <Label htmlFor="isActive">Active</Label>
                </div>
                <Button type="submit">{isNew ? "Create" : "Update"}</Button>
            </form>
        </div>
    );
}