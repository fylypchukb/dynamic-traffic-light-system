"use client";

import React, { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Switch } from "@/components/ui/switch";
import { intersectionApi } from "@/lib/api";
import { Intersection } from "@/lib/api-types";
import { PageLoading } from "@/components/layout/page-loading";
import { useToast } from "@/hooks/use-toast";

const formSchema = z.object({
  city: z.string().min(1, "City is required"),
  location: z.string().min(1, "Location is required"),
  isActive: z.boolean(),
});

interface IntersectionFormProps {
  id: number | null;
}

export function IntersectionForm({ id }: IntersectionFormProps) {
  const [isLoading, setIsLoading] = useState(id !== null);
  const [intersection, setIntersection] = useState<Intersection | null>(null);
  const router = useRouter();
  const { toast } = useToast();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      city: "",
      location: "",
      isActive: true,
    },
  });

  useEffect(() => {
    async function fetchIntersection() {
      if (!id) return;
      
      try {
        const response = await intersectionApi.getById(id);
        setIntersection(response.result);
        form.reset({
          city: response.result.city || "",
          location: response.result.location || "",
          isActive: response.result.isActive,
        });
      } catch (error) {
        console.error("Failed to fetch intersection:", error);
        toast({
          title: "Error",
          description: "Failed to load intersection details",
          variant: "destructive",
        });
      } finally {
        setIsLoading(false);
      }
    }

    fetchIntersection();
  }, [id, form, toast]);

  async function onSubmit(values: z.infer<typeof formSchema>) {
    try {
      if (id) {
        await intersectionApi.update(id, values);
        toast({
          title: "Success",
          description: "Intersection updated successfully",
        });
      } else {
        await intersectionApi.create(values);
        toast({
          title: "Success",
          description: "Intersection created successfully",
        });
      }
      router.push("/intersections");
    } catch (error) {
      console.error("Failed to save intersection:", error);
      toast({
        title: "Error",
        description: "Failed to save intersection",
        variant: "destructive",
      });
    }
  }

  if (isLoading) {
    return <PageLoading />;
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <FormField
          control={form.control}
          name="city"
          render={({ field }) => (
            <FormItem>
              <FormLabel>City</FormLabel>
              <FormControl>
                <Input placeholder="Enter city name" {...field} />
              </FormControl>
              <FormDescription>
                The city where this intersection is located
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="location"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Location</FormLabel>
              <FormControl>
                <Input placeholder="Enter intersection location" {...field} />
              </FormControl>
              <FormDescription>
                The specific location or address of the intersection
              </FormDescription>
              <FormMessage />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name="isActive"
          render={({ field }) => (
            <FormItem className="flex flex-row items-center justify-between rounded-lg border p-4">
              <div className="space-y-0.5">
                <FormLabel className="text-base">Active Status</FormLabel>
                <FormDescription>
                  Whether this intersection is currently active in the system
                </FormDescription>
              </div>
              <FormControl>
                <Switch
                  checked={field.value}
                  onCheckedChange={field.onChange}
                />
              </FormControl>
            </FormItem>
          )}
        />

        <div className="flex justify-end gap-4">
          <Button
            type="button"
            variant="outline"
            onClick={() => router.push("/intersections")}
          >
            Cancel
          </Button>
          <Button type="submit">
            {id ? "Update Intersection" : "Create Intersection"}
          </Button>
        </div>
      </form>
    </Form>
  );
}